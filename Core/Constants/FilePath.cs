namespace FileUpload.API.Constants;
public class FilePath
{
    //public static string Root = "wwwroot\\Uploads\\Images\\";
    public static string Root = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Images");
}

