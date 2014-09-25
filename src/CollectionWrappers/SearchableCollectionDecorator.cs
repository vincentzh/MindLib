using System.Collections.Generic;

namespace MindHarbor.CollectionWrappers {
	/// <summary>
	/// a collection wrapper that wrappes a ICollection but enable search by property
	/// </summary>
	/// <remarks>
	/// it utilizes an index dictionary (which is built when first serach)
	/// </remarks>
	public class SearchableCollectionDecorator<PropT, CollectionItemT> :
		SearchableCollectionDecoratorBase<PropT, CollectionItemT> where CollectionItemT : class {
		protected readonly ICollection<CollectionItemT> innerCollection;

		public SearchableCollectionDecorator(ICollection<CollectionItemT> innerCollection, string searchPropertyName)
			: base(searchPropertyName) {
			this.innerCollection = innerCollection;
		}

		protected override ICollection<CollectionItemT> InnerCollection {
			get { return innerCollection; }
		}
		}

	public class SearchableSetDecorator<PropT, CollectionItemT> : SearchableCollectionDecoratorBase<PropT, CollectionItemT>,
	                                                              ISet<CollectionItemT> where CollectionItemT : class {
		protected readonly ISet<CollectionItemT> innerCollection;

		public SearchableSetDecorator(ISet<CollectionItemT> innerCollection, string searchPropertyName)
			: base(searchPropertyName) {
			this.innerCollection = innerCollection;
		}

		protected override ICollection<CollectionItemT> InnerCollection {
			get { return innerCollection; }
		}

		#region ISet<CollectionItemT> Members

		public Iesi.Collections.Generic.ISet<CollectionItemT> Union(Iesi.Collections.Generic.ISet<CollectionItemT> a) {
			return innerCollection.Union(a);
		}

		public Iesi.Collections.Generic.ISet<CollectionItemT> Intersect(Iesi.Collections.Generic.ISet<CollectionItemT> a) {
			return innerCollection.Intersect(a);
		}

		public Iesi.Collections.Generic.ISet<CollectionItemT> Minus(Iesi.Collections.Generic.ISet<CollectionItemT> a) {
			return innerCollection.Minus(a);
		}

		public Iesi.Collections.Generic.ISet<CollectionItemT> ExclusiveOr(Iesi.Collections.Generic.ISet<CollectionItemT> a) {
			return innerCollection.ExclusiveOr(a);
		}

		public bool ContainsAll(ICollection<CollectionItemT> c) {
			return innerCollection.ContainsAll(c);
		}

		bool Iesi.Collections.Generic.ISet<CollectionItemT>.Add(CollectionItemT o) {
			return innerCollection.Add(o);
		}

	    public void UnionWith(IEnumerable<CollectionItemT> other)
	    {
	       innerCollection.UnionWith(other); 
	    }

	    public void IntersectWith(IEnumerable<CollectionItemT> other)
	    {
	       innerCollection.IntersectWith(other);
	    }

	    public void ExceptWith(IEnumerable<CollectionItemT> other)
	    {
	        innerCollection.ExceptWith(other);
	    }

	    public void SymmetricExceptWith(IEnumerable<CollectionItemT> other)
	    {
	        
	    }

	    public bool IsSubsetOf(IEnumerable<CollectionItemT> other)
	    {
	        throw new System.NotImplementedException();
	    }

	    public bool IsSupersetOf(IEnumerable<CollectionItemT> other)
	    {
	        throw new System.NotImplementedException();
	    }

	    public bool IsProperSupersetOf(IEnumerable<CollectionItemT> other)
	    {
	        throw new System.NotImplementedException();
	    }

	    public bool IsProperSubsetOf(IEnumerable<CollectionItemT> other)
	    {
	        throw new System.NotImplementedException();
	    }

	    public bool Overlaps(IEnumerable<CollectionItemT> other)
	    {
	        throw new System.NotImplementedException();
	    }

	    public bool SetEquals(IEnumerable<CollectionItemT> other)
	    {
	        throw new System.NotImplementedException();
	    }

	    public bool AddAll(ICollection<CollectionItemT> c) {
			return innerCollection.AddAll(c);
		}

		public bool RemoveAll(ICollection<CollectionItemT> c) {
			return innerCollection.RemoveAll(c);
		}

		public bool RetainAll(ICollection<CollectionItemT> c) {
			return innerCollection.RetainAll(c);
		}

		public bool IsEmpty {
			get { return innerCollection.IsEmpty; }
		}

		public object Clone() {
			return
				new SearchableSetDecorator<PropT, CollectionItemT>((Iesi.Collections.Generic.ISet<CollectionItemT>) innerCollection.Clone(),
				                                                   SearchPropertyName);
		}

		#endregion

	    #region Implementation of ISet<CollectionItemT>

	    public bool Add(CollectionItemT item)
	    {
	        throw new System.NotImplementedException();
	    }

	    #endregion
	                                                              }
}