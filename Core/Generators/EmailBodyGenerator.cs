namespace Core.Generators
{
    public class EmailBodyGenerator
    {
        public static string SendActiveEmail(string userName, string activeCode)
        {

            string body = @"
                        <div style=""direction:rtl;padding20px"">
                            <h1>UPCode</h1>
                            <h2> Hi UserName !</h2>
                            <i>DateTime</i>
                            <p>Tanks for register in <a href=""#"">UPCode</a> , You must activate your account to continue .</p>
                            <h3 style=""color:forestgreen;""><a href=""http://localhost:5074/account/activeaccount/ActiveCode"">Account activation !</a></h3>
                        </div>";
            body = body.Replace("UserName", userName);
            body = body.Replace("ActiveCode", activeCode);
            body = body.Replace("DateTime", DateTime.Now.ToString());

            return body;
        }
        public static string SendResetPaswordEmail(string userName, string activeCode)
        {

            string body = @"
                    <div style=""direction:rtl;padding20px"">
                        <h1>UPCode</h1>
                        <h2>Hi UserName</h2>
                        <i>DateTime</i>
                        <p> Click the link below to recover your password</p>
                        <h3 style=""color:forestgreen;""><a href=""http://localhost:5074/account/ResetPassword/ActiveCode"">Password recovery!</a></h3>
                    </div>";
            body = body.Replace("UserName", userName);
            body = body.Replace("ActiveCode", activeCode);
            body = body.Replace("DateTime", DateTime.Now.ToString());

            return body;
        }
    }
}
