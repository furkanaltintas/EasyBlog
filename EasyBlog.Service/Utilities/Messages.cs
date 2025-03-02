﻿namespace EasyBlog.Service.Utilities;

public static class Messages
{
    public static class General
    {
        public static string ValidationError()
        {
            return "Bir veya daha fazla validasyon hatası ile karşılaşıldı.";
        }
    }

    public static class Article
    {
        public static string NotFound(bool isPlural)
        {
            if (isPlural) return "Hiç bir makale bulunamadı";
            return "Böyle bir makale bulunamadı";
        }

        public static string NotFoundById(int articleId)
        {
            return $"{articleId} makale koduna ait bir makale bulunamadı.";
        }

        public static string Add(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla eklenmiştir.";
        }

        public static string Update(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla güncellenmiştir.";
        }

        public static string Delete(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla silinmiştir.";
        }

        public static string HardDelete(string articleTitle)
        {
            return $"{articleTitle} adlı makale veritabanından silinmiştir.";
        }

        public static string UndoDelete(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla arşivden geri getirilmiştir.";
        }
    }
}