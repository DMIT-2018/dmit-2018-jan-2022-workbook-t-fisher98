using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SamplePages
{
    public class BasicsModel : PageModel
    {
        // This is basically an object, treat it as such.

        // It will have:

        // - Data fields
        public string MyName;

        // - Properties
        // The annotation [TempData] stores data until it's read in another
        // immediate request
        // This annotation attribute has two methods called Keep(string) and 
        // Peek(string) (used on Content page)
        // Keep in a dictionary (name/value pair)
        // useful to redirect when data is required for more than a single request
        // Implemented by TempData providers using either cookies or session state
        // TempData is NOT bound to any particular control like BindProperty
        [TempData]
        public string FeedBack { get; set; }

        // The annotation BindProperty ties a property in the PageModel class
        // directly to a control on the ContentPage.
        // Data is transferred between the two automatically
        // On the Content page, the control to use this property will have
        // a helper-tag called asp-for

        // To retain a value in the control tied to this property AND retained
        // via the @page, use the SupportsGet attribute = true
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        // - Constructors

        // - Functions, Methods, Behaviours

        public void OnGet()
        {
            // Executes in response to a Get Request from the browser
            // When the page is "first" accessed, the browser issues a Get Request
            // When the page is refreshed, WITHOUT a Post request, the browser issues
            // a Get Request.
            // When the page is retrieved in response to a form's POST using RedirecToPage()
            // If NO RedirectToPage() is used on the POST, there is NO Get request issued

            // Server-side processing
            // Contains no HTML

            Console.WriteLine("OnGet()");

            Random rnd = new Random();
            int oddeven = rnd.Next(0, 25);

            if(oddeven % 2 == 0)
            {
                MyName = $"Tyler is even {oddeven}";
            }
            else
            {
                MyName = null;
            }
        }

        // Processing in response to a request from a form on a web page
        // This request is referred to as a Post (method="post")
        
        // General Post
        // A general post occurs when a asp-pagehandler is NOT used
        // The return datatype can be void, however, you will normally
        // encounter the datatype IActionResult
        // The IActionResult requires some type of request action
        // on the return statement of the method OnPost()
        // Typical actions: 
        // Page()
        //  - Does NOT issue an OnGet request
        //  - Remains on the current page
        //  - A good action for form processing involving validation
        //      and with the catch of a try/catch
        // RedirectToPage()
        //  - DOES issue an OnGet request
        //  - is used to retain unput values via the @page and your BindProperty
        public IActionResult OnPost()
        {
            // This line of is used to cause a delay in processing
            // so that we can see on the Network Activity some type of simulated processing
            Thread.Sleep(2000);

            // Retrieve data via the Request object
            // Request: web page to server
            // Response: server to web page
            string buttonValue = Request.Form["theButton"];
            FeedBack = $"Button pressed is {buttonValue} with numeric input of {id}";
            //return Page(); // does not issue an OnGet() request
            return RedirectToPage(new {id = id});
        }
    }
}
