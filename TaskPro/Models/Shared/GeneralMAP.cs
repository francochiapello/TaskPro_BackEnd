using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskPro.Data;

namespace TaskPro.Models.Shared
{

    public static class GeneralMAP
    {
        public static string toString(this DateTime? date)
        {
            if (date is null)
            {
                return string.Empty;
            }
            else
            {
                return date.Value.ToString("dd-MM-yyyy");
            }
        }

        public static string dateString(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }
        public static IEnumerable<Error> AllErrors(this ModelStateDictionary modelState)
        {
            var result = new List<Error>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                                   .Select(error => new Error(fieldKey, error.ErrorMessage));
                result.AddRange(fieldErrors);
            }

            return result;
        }

        public static Usuario? getUser(this int id)
        {
            using (DbgrpContext db1 = new DbgrpContext())
            {
                var usuario = db1.Usuarios.Where(x => x.Id.Equals(id)).FirstOrDefault();
                return usuario;
            }
        }
    }
}
