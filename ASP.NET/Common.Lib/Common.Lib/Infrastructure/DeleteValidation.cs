using System;
using System.Collections.Generic;
using System.Text;
using Common.Lib.Core;


namespace Common.Lib.Infrastructure
{
	public class DeleteValidation<T> where T : Entity
	{
		public ValidationResult Validation { get; set; } = new ValidationResult();

		public bool DeleteValidationSuccesful
		{
			get
			{
				return Validation.ValidationSuccesful;
			}

			set
			{
				Validation.ValidationSuccesful = value;
			}
		}
		public List<string> DeleteValidationMessages { get; set; } = new List<string>();

		public T Entity { get; set; }


		//constructor para que sea true por defecto
		public DeleteValidation()
		{

		}
		public DeleteValidation(bool initTrue)
		{
			this.DeleteValidationSuccesful = initTrue;
		}



		public DeleteValidation<TOut> Cast<TOut>() where TOut : Entity
		{
			var output = new DeleteValidation<TOut>
			{
				Entity = this.Entity as TOut,
				Validation = this.Validation
			};

			return output;
		}
	}
}
