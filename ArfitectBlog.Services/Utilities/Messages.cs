using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArfitectBlog.Services.Utilities
{
    public static class Messages
    {
        // Messages.Category.NotFound()
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir kategori bulunamadı.";
                return "Böyle bir kategori bulunamadı.";
            }

            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklenmiştir.";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla güncellenmiştir.";
            }
            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            }
            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla veritabanından silinmiştir.";
            }
            public static string UndoDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla arşivden geri getirilmiştir.";
            }
        }

        public static class Post
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "paylaşımlar bulunamadı.";
                return "Böyle bir paylaşım bulunamadı.";
            }
            public static string Add(string postTitle)
            {
                return $"{postTitle} başlıklı paylaşım başarıyla eklenmiştir.";
            }

            public static string Update(string postTitle)
            {
                return $"{postTitle} başlıklı paylaşım başarıyla güncellenmiştir.";
            }
            public static string UndoDelete(string postTitle)
            {
                return $"{postTitle} başlıklı paylaşım başarıyla geri getirilmiştir.";
            }
            public static string Delete(string postTitle)
            {
                return $"{postTitle} başlıklı paylaşım başarıyla silinmiştir.";
            }
            public static string HardDelete(string postTitle)
            {
                return $"{postTitle} başlıklı paylaşım başarıyla veritabanından silinmiştir.";
            }
            public static string IncreaseViewCount(string postTitle)
            {
                return $"{postTitle} başlıklı paylaşımın okunma sayısı başarıyla artırılmıştır.";
            }
        }
        public static class Comment
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiç bir yorum bulunamadı.";
                return "Böyle bir yorum bulunamadı.";
            }

            public static string Approve(int commentId)
            {
                return $"{commentId} no'lu, yorum başarıyla onaylanmıştır.";
            }
            public static string Add(string createdByName)
            {
                return $"{createdByName}, yorum başarıyla eklenmiştir.";
            }

            public static string Update(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla güncellenmiştir.";
            }
            public static string Delete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla silinmiştir.";
            }
            public static string UndoDelete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla arşivden geri getirilmiştir.";
            }
            public static string HardDelete(string createdByName)
            {
                return $"{createdByName} tarafından eklenen yorum başarıyla veritabanından silinmiştir.";
            }
        }
    }
}
