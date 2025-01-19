using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        coupon ??= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        logger.LogInformation("Discount was retrieved for ProductName : {ProductName}, Amount : {Amount}",
            coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        var nameExists = await dbContext.Coupons
            .AnyAsync(x => x.Id != coupon.Id && x.ProductName == coupon.ProductName);

        if (nameExists)
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                $"Discount with ProductName='{coupon.ProductName}' already exists, ProductName must be unique"));

        dbContext.Coupons.Add(coupon);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount was successfully created. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>()
            ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        if (coupon.Id == 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "No Id was provided."));

        var exists = await dbContext.Coupons.AnyAsync(x => x.Id == coupon.Id);
        if (!exists)
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id={coupon.Id} not found."));

        var nameExists = await dbContext.Coupons
            .AnyAsync(x => x.Id != coupon.Id && x.ProductName == coupon.ProductName);

        if (nameExists)
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                $"Discount with ProductName='{coupon.ProductName}' already exists, ProductName must be unique"));

        dbContext.Coupons.Update(coupon);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount was successfully updated. ProductName : {ProductName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName='{request.ProductName}' was not found."));

        dbContext.Coupons.Remove(coupon);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount was successfully deleted. ProductName : {ProductName}", request.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }
}
