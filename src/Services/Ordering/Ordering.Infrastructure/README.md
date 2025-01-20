## Migration
- Set the Default Project in Package Manager Console to the Ordering.Infrastructure project
- First install nuget `Microsoft.EntityFrameworkCore.Design`
- run: `Add-Migration InitialCreate -OutputDir Data/Migrations -Project Ordering.Infrastructure -StartupProject Ordering.API`
- run: `Update-Database`

## TODO
- Add precision to `decimal` type fields
 - No store type was specified for the decimal property 'Price' on entity type 'Product'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
- Address the enum field
 - The 'OrderStatus' property 'Status' on entity type 'Order' is configured with a database-generated default, but has no configured sentinel value. The database-generated default will always be used for inserts when the property has the value '0', since this is the CLR default for the 'OrderStatus' type. Consider using a nullable type, using a nullable backing field, or setting the sentinel value for the property to ensure the database default is used when, and only when, appropriate. See https://aka.ms/efcore-docs-default-values for more information.