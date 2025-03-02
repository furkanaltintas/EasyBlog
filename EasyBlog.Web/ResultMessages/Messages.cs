namespace EasyBlog.Web.ResultMessages;

public static class Messages
{
    public static class ArticleMessage
    {
        public static string Add(string articleTitle) { return $"{articleTitle} başlıklı makale başarıyla eklendi."; }
        public static string Update(string articleTitle) { return $"{articleTitle} başlıklı makale başarıyla güncellenmiştir."; }
        public static string Delete(string articleTitle) { return $"{articleTitle} başlıklı makale başarıyla silinmiştir."; }
    }
}