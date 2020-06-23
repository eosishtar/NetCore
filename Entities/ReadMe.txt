1. Run the following command in Package Manager to update the entities 

# Please use this
Scaffold-DbContext "Server=.;Database=NetCore;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -Context NCContext -Project Core -OutputDir Entities -Schema brm -Force -StartupProject NetCore.WebApi -DataAnnotations
