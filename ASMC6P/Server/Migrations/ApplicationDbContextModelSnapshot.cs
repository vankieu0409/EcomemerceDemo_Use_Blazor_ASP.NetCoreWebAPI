﻿// <auto-generated />
using System;
using ASMC6P.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASMC6P.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ASMC6P.Shared.Entities.CartItemEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("03b15145-6597-4a71-82c7-14e36cfd6780"),
                            IsDeleted = false,
                            Name = "Books",
                            Url = "books",
                            Visible = true
                        },
                        new
                        {
                            Id = new Guid("6faadb7d-ef79-4f02-8fba-b25e76f70a2c"),
                            IsDeleted = false,
                            Name = "Movies",
                            Url = "movies",
                            Visible = true
                        },
                        new
                        {
                            Id = new Guid("8dbb355f-bee4-4e1d-8081-fcfefebec213"),
                            IsDeleted = false,
                            Name = "Video Games",
                            Url = "video-games",
                            Visible = true
                        });
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.ImageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.OrderItemEntity", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("NewPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8f6f4d09-4292-48cc-a8bf-76e7fe0bb961"),
                            CategoryId = new Guid("03b15145-6597-4a71-82c7-14e36cfd6780"),
                            Description = "The Hitchhiker's Guide to the Galaxy[note 1] (sometimes referred to as HG2G,[1] HHGTTG,[2] H2G2,[3] or tHGttG) is a comedy science fiction franchise created by Douglas Adams. Originally a 1978 radio comedy broadcast on BBC Radio 4, it was later adapted to other formats, including stage shows, novels, comic books, a 1981 TV series, a 1984 text-based computer game, and 2005 feature film.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
                            IsDeleted = false,
                            Name = "The Hitchhiker's Guide to the Galaxy",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5003)
                        },
                        new
                        {
                            Id = new Guid("e1cee7b3-a6ae-4ae3-bcf4-1955129d8197"),
                            CategoryId = new Guid("03b15145-6597-4a71-82c7-14e36cfd6780"),
                            Description = "Ready Player One is a 2011 science fiction novel, and the debut novel of American author Ernest Cline. The story, set in a dystopia in 2045, follows protagonist Wade Watts on his search for an Easter egg in a worldwide virtual reality game, the discovery of which would lead him to inherit the game creator's fortune. Cline sold the rights to publish the novel in June 2010, in a bidding war to the Crown Publishing Group (a division of Random House).[1] The book was published on August 16, 2011.[2] An audiobook was released the same day; it was narrated by Wil Wheaton, who was mentioned briefly in one of the chapters.[3][4]Ch. 20 In 2012, the book received an Alex Award from the Young Adult Library Services Association division of the American Library Association[5] and won the 2011 Prometheus Award.[6]",
                            Image = "https://upload.wikimedia.org/wikipedia/en/a/a4/Ready_Player_One_cover.jpg",
                            IsDeleted = false,
                            Name = "Ready Player One",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5022)
                        },
                        new
                        {
                            Id = new Guid("772bc058-2e46-4d3f-ac02-3dd4a6a5aced"),
                            CategoryId = new Guid("03b15145-6597-4a71-82c7-14e36cfd6780"),
                            Description = "Nineteen Eighty-Four (also stylised as 1984) is a dystopian social science fiction novel and cautionary tale written by English writer George Orwell. It was published on 8 June 1949 by Secker & Warburg as Orwell's ninth and final book completed in his lifetime. Thematically, it centres on the consequences of totalitarianism, mass surveillance and repressive regimentation of people and behaviours within society.[2][3] Orwell, a democratic socialist, modelled the totalitarian government in the novel after Stalinist Russia and Nazi Germany.[2][3][4] More broadly, the novel examines the role of truth and facts within politics and the ways in which they are manipulated.",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/c/c3/1984first.jpg",
                            IsDeleted = false,
                            Name = "Nineteen Eighty-Four",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5026)
                        },
                        new
                        {
                            Id = new Guid("71b5e16d-7c46-41b8-9b5a-cb00376c4b60"),
                            CategoryId = new Guid("6faadb7d-ef79-4f02-8fba-b25e76f70a2c"),
                            Description = "The Matrix is a 1999 science fiction action film written and directed by the Wachowskis, and produced by Joel Silver. Starring Keanu Reeves, Laurence Fishburne, Carrie-Anne Moss, Hugo Weaving, and Joe Pantoliano, and as the first installment in the Matrix franchise, it depicts a dystopian future in which humanity is unknowingly trapped inside a simulated reality, the Matrix, which intelligent machines have created to distract humans while using their bodies as an energy source. When computer programmer Thomas Anderson, under the hacker alias \"Neo\", uncovers the truth, he \"is drawn into a rebellion against the machines\" along with other people who have been freed from the Matrix.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/c/c1/The_Matrix_Poster.jpg",
                            IsDeleted = false,
                            Name = "The Matrix",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5031)
                        },
                        new
                        {
                            Id = new Guid("0e7f6b60-6e98-4220-93ea-45f64e0f1aa2"),
                            CategoryId = new Guid("6faadb7d-ef79-4f02-8fba-b25e76f70a2c"),
                            Description = "Back to the Future is a 1985 American science fiction film directed by Robert Zemeckis. Written by Zemeckis and Bob Gale, it stars Michael J. Fox, Christopher Lloyd, Lea Thompson, Crispin Glover, and Thomas F. Wilson. Set in 1985, the story follows Marty McFly (Fox), a teenager accidentally sent back to 1955 in a time-traveling DeLorean automobile built by his eccentric scientist friend Doctor Emmett \"Doc\" Brown (Lloyd). Trapped in the past, Marty inadvertently prevents his future parents' meeting—threatening his very existence—and is forced to reconcile the pair and somehow get back to the future.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/d/d2/Back_to_the_Future.jpg",
                            IsDeleted = false,
                            Name = "Back to the Future",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5036)
                        },
                        new
                        {
                            Id = new Guid("913f130b-c1b6-4a98-8d63-4cf316da3fc1"),
                            CategoryId = new Guid("6faadb7d-ef79-4f02-8fba-b25e76f70a2c"),
                            Description = "Toy Story is a 1995 American computer-animated comedy film produced by Pixar Animation Studios and released by Walt Disney Pictures. The first installment in the Toy Story franchise, it was the first entirely computer-animated feature film, as well as the first feature film from Pixar. The film was directed by John Lasseter (in his feature directorial debut), and written by Joss Whedon, Andrew Stanton, Joel Cohen, and Alec Sokolow from a story by Lasseter, Stanton, Pete Docter, and Joe Ranft. The film features music by Randy Newman, was produced by Bonnie Arnold and Ralph Guggenheim, and was executive-produced by Steve Jobs and Edwin Catmull. The film features the voices of Tom Hanks, Tim Allen, Don Rickles, Wallace Shawn, John Ratzenberger, Jim Varney, Annie Potts, R. Lee Ermey, John Morris, Laurie Metcalf, and Erik von Detten. Taking place in a world where anthropomorphic toys come to life when humans are not present, the plot focuses on the relationship between an old-fashioned pull-string cowboy doll named Woody and an astronaut action figure, Buzz Lightyear, as they evolve from rivals competing for the affections of their owner, Andy Davis, to friends who work together to be reunited with Andy after being separated from him.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/1/13/Toy_Story.jpg",
                            IsDeleted = false,
                            Name = "Toy Story",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5041)
                        },
                        new
                        {
                            Id = new Guid("453f44f7-e33e-4477-a2ad-a78e232db482"),
                            CategoryId = new Guid("8dbb355f-bee4-4e1d-8081-fcfefebec213"),
                            Description = "Half-Life 2 is a 2004 first-person shooter game developed and published by Valve. Like the original Half-Life, it combines shooting, puzzles, and storytelling, and adds features such as vehicles and physics-based gameplay.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/2/25/Half-Life_2_cover.jpg",
                            IsDeleted = false,
                            Name = "Half-Life 2",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5045)
                        },
                        new
                        {
                            Id = new Guid("5cda1ac6-7e1f-4f28-84e9-319455fc7016"),
                            CategoryId = new Guid("8dbb355f-bee4-4e1d-8081-fcfefebec213"),
                            Description = "Diablo II is an action role-playing hack-and-slash computer video game developed by Blizzard North and published by Blizzard Entertainment in 2000 for Microsoft Windows, Classic Mac OS, and macOS.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/d/d5/Diablo_II_Coverart.png",
                            IsDeleted = false,
                            Name = "Diablo II",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5049)
                        },
                        new
                        {
                            Id = new Guid("26429623-f8b1-49de-85a3-57e2c370afcb"),
                            CategoryId = new Guid("8dbb355f-bee4-4e1d-8081-fcfefebec213"),
                            Description = "Day of the Tentacle, also known as Maniac Mansion II: Day of the Tentacle, is a 1993 graphic adventure game developed and published by LucasArts. It is the sequel to the 1987 game Maniac Mansion.",
                            Image = "https://upload.wikimedia.org/wikipedia/en/7/79/Day_of_the_Tentacle_artwork.jpg",
                            IsDeleted = false,
                            Name = "Day of the Tentacle",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5053)
                        },
                        new
                        {
                            Id = new Guid("ff619bfc-5866-4a5b-9a95-9a8a0e457c91"),
                            CategoryId = new Guid("8dbb355f-bee4-4e1d-8081-fcfefebec213"),
                            Description = "The Xbox is a home video game console and the first installment in the Xbox series of video game consoles manufactured by Microsoft.",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/4/43/Xbox-console.jpg",
                            IsDeleted = false,
                            Name = "Xbox",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5056)
                        },
                        new
                        {
                            Id = new Guid("285a8637-e594-4729-b408-67819bb3e3e9"),
                            CategoryId = new Guid("8dbb355f-bee4-4e1d-8081-fcfefebec213"),
                            Description = "The Super Nintendo Entertainment System (SNES), also known as the Super NES or Super Nintendo, is a 16-bit home video game console developed by Nintendo that was released in 1990 in Japan and South Korea.",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/e/ee/Nintendo-Super-Famicom-Set-FL.jpg",
                            IsDeleted = false,
                            Name = "Super Nintendo Entertainment System",
                            NewPrice = 123m,
                            OriginalPrice = 345m,
                            Quantity = 1,
                            UploadedDate = new DateTime(2023, 4, 6, 22, 46, 25, 774, DateTimeKind.Local).AddTicks(5060)
                        });
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("ab251560-a455-40fd-adfd-54f9e150f874"),
                            ConcurrencyStamp = "8bcc8b75-c547-4736-9c12-7d6e40127eef",
                            Name = "Administrator",
                            NormalizedName = "Administrator"
                        },
                        new
                        {
                            Id = new Guid("8d4b836e-d9fa-4fa9-88c0-9a875d2b7d5c"),
                            ConcurrencyStamp = "859deee4-e8d3-417c-a27d-d1723f9b8960",
                            Name = "Employee",
                            NormalizedName = "Employee"
                        },
                        new
                        {
                            Id = new Guid("f91ec0e5-d768-42e2-8926-de7d3162430f"),
                            ConcurrencyStamp = "6976fea4-c67e-423b-bf41-c2f5e8790ed3",
                            Name = "Customer",
                            NormalizedName = "Customer"
                        });
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Decriptions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("fb1eab16-920e-4480-b3ee-01f6e9c15ab5"),
                            AccessFailedCount = 0,
                            Address = "Tuân Chính -Vĩnh Tường - Vĩnh Phúc",
                            ConcurrencyStamp = "b7b6638d-ce6a-4efb-a4bb-28b51d2ee08f",
                            Decriptions = "",
                            DisplayName = "Bậu",
                            Email = "vankieu0409@gmail.com",
                            EmailConfirmed = true,
                            Image = "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2022/8/18/1082204/Leesuk.jpg",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            LockoutEnd = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            NormalizedEmail = "vankieu0409@gmail.com",
                            NormalizedUserName = "vankieu0409@gmail.com",
                            PasswordHash = "AQAAAAEAACcQAAAAEMfrd51YGMSLzKs7NWUztJV/CKxRqpABxKVBI7+iwpeD82bZA8aBCnr7kKusapiDQw==",
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1722fa12-3a41-48fc-8da2-f6ff094daf0b",
                            TwoFactorEnabled = false,
                            UserName = "vankieu0409@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("6aa93c41-f21f-44e3-8f46-7d76b03574c5"),
                            AccessFailedCount = 0,
                            Address = "Tuân Chính -Vĩnh Tường - Vĩnh Phúc",
                            ConcurrencyStamp = "ce8dc22b-3c5f-4bcd-a7cd-ef67858c503e",
                            Decriptions = " Chị rất nóng tính",
                            DisplayName = "Chị Nhà Cục Súc",
                            Email = "kieunvph14806@fpt.edu.vn",
                            EmailConfirmed = true,
                            Image = "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2022/8/18/1082204/Leesuk.jpg",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            LockoutEnd = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            NormalizedEmail = "kieunvph14806@fpt.edu.vn",
                            NormalizedUserName = "kieunvph14806@fpt.edu.vn",
                            PasswordHash = "AQAAAAEAACcQAAAAEMfrd51YGMSLzKs7NWUztJV/CKxRqpABxKVBI7+iwpeD82bZA8aBCnr7kKusapiDQw==",
                            PhoneNumber = "",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "653ee3af-192f-4044-84ed-09e9804eb345",
                            TwoFactorEnabled = false,
                            UserName = "kieunvph14806@fpt.edu.vn"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("fb1eab16-920e-4480-b3ee-01f6e9c15ab5"),
                            RoleId = new Guid("ab251560-a455-40fd-adfd-54f9e150f874")
                        },
                        new
                        {
                            UserId = new Guid("fb1eab16-920e-4480-b3ee-01f6e9c15ab5"),
                            RoleId = new Guid("8d4b836e-d9fa-4fa9-88c0-9a875d2b7d5c")
                        },
                        new
                        {
                            UserId = new Guid("6aa93c41-f21f-44e3-8f46-7d76b03574c5"),
                            RoleId = new Guid("f91ec0e5-d768-42e2-8926-de7d3162430f")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.OrderItemEntity", b =>
                {
                    b.HasOne("ASMC6P.Shared.Entities.OrderEntity", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASMC6P.Shared.Entities.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.ProductEntity", b =>
                {
                    b.HasOne("ASMC6P.Shared.Entities.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ASMC6P.Shared.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("ASMC6P.Shared.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("ASMC6P.Shared.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ASMC6P.Shared.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASMC6P.Shared.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("ASMC6P.Shared.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ASMC6P.Shared.Entities.OrderEntity", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
