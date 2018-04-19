using System.Collections.Generic;

namespace Common.ViewModels
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Errors=new List<string>();
            Redirect=new Redirect();
        }
        public int Code { get; set; }
        public bool IsSucced { get; set; }
        public string Header { get; set; }
        public List<string> Errors { get; set; }
        public Redirect Redirect { get; set; }
    }

    public class Redirect
    {
        public Redirect()
        {
            Redirected = false;
        }
        public bool Redirected { get; set; }
        public string RedirectLink { get; set; }
    }
}