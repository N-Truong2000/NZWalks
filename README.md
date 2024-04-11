# 1. Cai cac thu vien can thiet:
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Proxies

# 2. Su dung cau lenh command ben duoi:
Scaffold-DbContext "Server=TRUONG\SQLEXPRESS;Database=Bcrypt;user id=sa;password=1234567;trusted_connection=true;encrypt=false" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entity -Context DatabaseContext -f
 Add-Migration "Creating Auth Database" -context "<name>"
-f la co dung de override class da co trong thu muc Model
# 3.Chuoi ket noi:  
"ConnectionStrings": {
    "Default": "Server=TRUONG\\SQLEXPRESS;Database=AspnetIdentityV2;User ID=sa;Password=1234567;TrustServerCertificate=True;Encrypt=False"
  },
  
  [tete](https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pinterest.com%2Fpin%2F687502699376361132%2F&psig=AOvVaw2P2-S444bVRwqZbPE7TWL8&ust=1712933287318000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCPjAgPSzuoUDFQAAAAAdAAAAABAE)