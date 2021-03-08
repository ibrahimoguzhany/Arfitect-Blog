using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBlog.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "VARBINARY(500)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedByName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[,]
                {
                    { 1, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 8, DateTimeKind.Local).AddTicks(821), "C# programlama dili ile ilgili en guncel bilgiler", true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 8, DateTimeKind.Local).AddTicks(830), "C#", "C# Blog Kategorisi" },
                    { 2, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 8, DateTimeKind.Local).AddTicks(843), "C++ programlama dili ile ilgili en guncel bilgiler", true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 8, DateTimeKind.Local).AddTicks(844), "C++", "C++ Blog Kategorisi" },
                    { 3, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 8, DateTimeKind.Local).AddTicks(848), "JavaScript programlama dili ile ilgili en guncel bilgiler", true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 8, DateTimeKind.Local).AddTicks(849), "JavaScript", "JavaScript Blog Kategorisi" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Name", "Note" },
                values: new object[] { 1, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 10, DateTimeKind.Local).AddTicks(9440), "Admin rolu tum haklara sahiptir.", true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 10, DateTimeKind.Local).AddTicks(9449), "Admin", "Admin Roludur." });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "Description", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedByName", "ModifiedDate", "Note", "PasswordHash", "Picture", "RoleId", "UserName" },
                values: new object[] { 1, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 21, DateTimeKind.Local).AddTicks(2372), "Ilk admin kullanici", "oguzhan@gmail.com", "Oguzhan", true, false, "Yilmaz", "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 21, DateTimeKind.Local).AddTicks(2383), "Admin kullanicisi", new byte[] { 50, 48, 50, 99, 98, 57, 54, 50, 97, 99, 53, 57, 48, 55, 53, 98, 57, 54, 52, 98, 48, 55, 49, 53, 50, 100, 50, 51, 52, 98, 55, 48 }, "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU", 1, "oguzhany" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 1, 1, 1, "Yinelenen bir sayfa içeriğinin okuyucunun dikkatini dağıttığı bilinen bir gerçektir. Lorem Ipsum kullanmanın amacı, sürekli 'buraya metin gelecek, buraya metin gelecek' yazmaya kıyasla daha dengeli bir harf dağılımı sağlayarak okunurluğu artırmasıdır. Şu anda birçok masaüstü yayıncılık paketi ve web sayfa düzenleyicisi, varsayılan mıgır metinler olarak Lorem Ipsum kullanmaktadır. Ayrıca arama motorlarında 'lorem ipsum' anahtar sözcükleri ile arama yapıldığında henüz tasarım aşamasında olan çok sayıda site listelenir. Yıllar içinde, bazen kazara, bazen bilinçli olarak (örneğin mizah katılarak), çeşitli sürümleri geliştirilmiştir", "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(4350), new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(2895), true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(5080), "C# 9.0 ve .NET 5 yenilikleri", "Oguzhan Yilmaz", "C# 9.0 ve .NET 5 yenilikleri", "C#, C# 9, .NET5, .NET Framework, .NET Core", "Default.jpg", "C# 9.0 ve .NET 5 yenilikleri", 1, 100 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 2, 2, 1, "Yaygın inancın tersine, Lorem Ipsum rastgele sözcüklerden oluşmaz. Kökleri M.Ö. 45 tarihinden bu yana klasik Latin edebiyatına kadar uzanan 2000 yıllık bir geçmişi vardır. Virginia'daki Hampden-Sydney College'dan Latince profesörü Richard McClintock, bir Lorem Ipsum pasajında geçen ve anlaşılması en güç sözcüklerden biri olan 'consectetur' sözcüğünün klasik edebiyattaki örneklerini incelediğinde kesin bir kaynağa ulaşmıştır. Lorm Ipsum, Çiçero tarafından M.Ö. 45 tarihinde kaleme alınan de Finibus Bonorum et Malor (İyi ve Kötünün Uç Sınırları) eserinin 1.10.32 ve 1.10.33 sayılı bölümlerinden gelmektedir. Bu kitap, ahlak kuramı üzerine bir tezdir ve Rönesans döneminde çok popüler olmuştur. Lorem Ipsum pasajının ilk satırı olan Lorem ipsum dolor sit amet 1.10.32 sayılı bölümdeki bir satırdan gelmektedir.", "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(6851), new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(6849), true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(6852), "C++ 11 ve 19 yenilikleri", "Oguzhan Yilmaz", "C++ 11 ve 19 yenilikleri", "C#, C# 9, .NET5, .NET Framework, .NET Core", "Default.jpg", "C++ 11 ve 19 yenilikleri", 1, 245 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "CommentCount", "Content", "CreatedByName", "CreatedDate", "Date", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "SeoAuthor", "SeoDescription", "SeoTags", "Thumbnail", "Title", "UserId", "ViewsCount" },
                values: new object[] { 3, 3, 1, "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.", "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(6859), new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(6857), true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 5, DateTimeKind.Local).AddTicks(6860), "JavaScript ES2019 ve ES2020 yenilikleri", "Oguzhan Yilmaz", "JavaScript ES2019 ve ES2020 yenilikleri", "JavaScript ES2019 ve ES2020 yenilikleri", "Default.jpg", "JavaScript ES2019 ve ES2020 yenilikleri", 1, 42 });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PostId", "Text" },
                values: new object[] { 1, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 9, DateTimeKind.Local).AddTicks(6910), true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 9, DateTimeKind.Local).AddTicks(6920), "C# Makale Yorumu", 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu ex non diam luctus pellentesque vitae malesuada dolor. Mauris sed fermentum ipsum, ut interdum tortor. Etiam pretium turpis et metus lacinia fringilla. Fusce vel leo quis felis molestie luctus eget sed nunc. In hac habitasse platea dictumst. Integer ex sem, egestas in dapibus nec, ullamcorper mattis nulla. Cras sodales nunc dignissim ligula euismod, in gravida tellus sollicitudin. Vivamus suscipit malesuada eleifend. Phasellus sodales augue risus, at fringilla est tincidunt non. Donec ut justo sed ligula iaculis vestibulum vitae sed massa. Pellentesque tortor quam, maximus in vehicula sed, dignissim vel leo. Quisque iaculis diam et est pulvinar rutrum. In eu hendrerit neque, non condimentum mi. Ut a egestas turpis" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PostId", "Text" },
                values: new object[] { 2, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 9, DateTimeKind.Local).AddTicks(6931), true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 9, DateTimeKind.Local).AddTicks(6933), "C++ Makale Yorumu", 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu ex non diam luctus pellentesque vitae malesuada dolor. Mauris sed fermentum ipsum, ut interdum tortor. Etiam pretium turpis et metus lacinia fringilla. Fusce vel leo quis felis molestie luctus eget sed nunc. In hac habitasse platea dictumst. Integer ex sem, egestas in dapibus nec, ullamcorper mattis nulla. Cras sodales nunc dignissim ligula euismod, in gravida tellus sollicitudin. Vivamus suscipit malesuada eleifend. Phasellus sodales augue risus, at fringilla est tincidunt non. Donec ut justo sed ligula iaculis vestibulum vitae sed massa. Pellentesque tortor quam, maximus in vehicula sed, dignissim vel leo. Quisque iaculis diam et est pulvinar rutrum. In eu hendrerit neque, non condimentum mi. Ut a egestas turpis" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CreatedByName", "CreatedDate", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedDate", "Note", "PostId", "Text" },
                values: new object[] { 3, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 9, DateTimeKind.Local).AddTicks(6936), true, false, "InitialCreate", new DateTime(2021, 3, 9, 1, 22, 46, 9, DateTimeKind.Local).AddTicks(6937), "JavaScript Makale Yorumu", 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu ex non diam luctus pellentesque vitae malesuada dolor. Mauris sed fermentum ipsum, ut interdum tortor. Etiam pretium turpis et metus lacinia fringilla. Fusce vel leo quis felis molestie luctus eget sed nunc. In hac habitasse platea dictumst. Integer ex sem, egestas in dapibus nec, ullamcorper mattis nulla. Cras sodales nunc dignissim ligula euismod, in gravida tellus sollicitudin. Vivamus suscipit malesuada eleifend. Phasellus sodales augue risus, at fringilla est tincidunt non. Donec ut justo sed ligula iaculis vestibulum vitae sed massa. Pellentesque tortor quam, maximus in vehicula sed, dignissim vel leo. Quisque iaculis diam et est pulvinar rutrum. In eu hendrerit neque, non condimentum mi. Ut a egestas turpis" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
