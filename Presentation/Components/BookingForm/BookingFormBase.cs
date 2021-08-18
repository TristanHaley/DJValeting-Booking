using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Presentation.Components.BookingForm
{
    public class BookingFormBase : ComponentBase
    {
        protected string    Firstname   { get; set; }
        protected string    Surname     { get; set; }
        protected int       Flexibility { get; set; }
        protected string    Email       { get; set; }
        protected string    Number      { get; set; }
        protected MudForm   Form        { get; set; }
        public    bool      FormValid   { get; set; }
        public    string[]  FormErrors  { get; set; }
        public    DateTime? When        { get; set; }
        public    int       VehicleType { get; set; }
    }
}