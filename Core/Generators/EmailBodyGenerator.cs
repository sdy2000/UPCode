namespace Core.Generators
{
    public class EmailBodyGenerator
    {
        public static string SendActiveEmail(string userName, string activeCode)
        {

            string body = @"<div style=""direction:rtl;padding20px"">
                        <h2>UserName عزیز !</h2>
                        <span>DateTime</span>
                        <p>با تشکر از ثبت نام شما در <a href=""#"">آپ کد</a> , جهت ادامه کار می بایست حساب کاربری خود را فعال کنید</p>
                        <h3 style=""color:forestgreen;""><a href=""http://localhost:3000/success-activation/ActiveCode"">فعال سازی حساب کاربری !</a></h3>
                        </div>";
            body = body.Replace("UserName", userName);
            body = body.Replace("ActiveCode", activeCode);
            body = body.Replace("DateTime", DateTime.Now.ToString());

            return body;
        }
    }
}
