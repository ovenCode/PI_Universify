using Microsoft.AspNetCore.Mvc;

namespace webapi
{
    public interface IListeners
    {
        public Task<NoContentResult> Update();
        public Task<IActionResult> Reset(Object item);
    }
}