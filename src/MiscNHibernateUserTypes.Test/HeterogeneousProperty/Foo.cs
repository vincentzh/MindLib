using System.Collections.Generic;

namespace MindHarbor.MiscNHibernateUserTypes.Test.HeterogeneousProperty
{
	public class Foo
	{
		private readonly IDictionary<string, MiscNHibernateUserTypes.HeterogeneousProperty> props =
				new Dictionary<string, MiscNHibernateUserTypes.HeterogeneousProperty>();

		public int Id { get; set; }

		public string Password { get; set; }

		public HeterogeneousPropertyDict Props
		{
			get { return new HeterogeneousPropertyDict(props); }
		}
	}
}