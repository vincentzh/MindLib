﻿using System;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;
using NHibernate.UserTypes;


namespace MindHarbor.SingletonUtil{
    /// <summary>
    /// A Hibernate userType for singleton value
    /// </summary>
    /// <remarks>
    /// This type depends on <see cref="SingletonRepository"/>
    /// </remarks>
    public class SingletonUserType : IUserType{
        public static readonly IType CustomeType = new CustomType(typeof (SingletonUserType),
                                                                  new Dictionary<string, string>());

        private readonly SqlType[] sqlTypes = new SqlType[] {new StringSqlType()};

        #region IUserType Members

        ///<summary>
        ///
        ///            Compare two instances of the class mapped by this type for persistent "equality"
        ///            ie. equality of persistent state
        ///            
        ///</summary>
        ///
        ///<param name="x"></param>
        ///<param name="y"></param>
        ///<returns>
        ///
        ///</returns>
        ///
        public new bool Equals(object x, object y){
            if (x != null && y != null)
                return x.GetType().Equals(y.GetType());
            return false;
        }

        /// <summary>
        /// Get a hashcode for the instance, consistent with persistence "equality" 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int GetHashCode(object x){
            return x.GetType().GetHashCode();
        }

        ///<summary>
        ///
        ///            Retrieve an instance of the mapped class from a JDBC resultset.
        ///            Implementors should handle possibility of null values.
        ///            
        ///</summary>
        ///
        ///<param name="rs">a IDataReader</param>
        ///<param name="names">column names</param>
        ///<param name="owner">the containing entity</param>
        ///<returns>
        ///
        ///</returns>
        ///
        ///<exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public object NullSafeGet(IDataReader rs, string[] names, object owner){
            string name = (string) NHibernateUtil.String.NullSafeGet(rs, names[0]);
            if (String.IsNullOrEmpty(name))
                return null;
            Type t = Type.GetType(name);
            if (t == null)
                throw new HibernateException("Singleton userType get failed, cannot find the type " + name);
            object retVal = SingletonRepository.Instance.Find(t);
            if (retVal == null)
                throw new HibernateException("Singleton userType get failed, the type " + name +
                                             " is not registered in the singletonRepository");
            return retVal;
        }

        ///<summary>
        ///
        ///            Write an instance of the mapped class to a prepared statement.
        ///            Implementors should handle possibility of null values.
        ///            A multi-column type should be written to parameters starting from index.
        ///            
        ///</summary>
        ///
        ///<param name="cmd">a IDbCommand</param>
        ///<param name="value">the object to write</param>
        ///<param name="index">command parameter index</param>
        ///<exception cref="T:NHibernate.HibernateException">HibernateException</exception>
        public void NullSafeSet(IDbCommand cmd, object value, int index){
            if (value == null)
                NHibernateUtil.String.NullSafeSet(cmd, null, index);
            else
                NHibernateUtil.String.NullSafeSet(cmd, GetTypeName(value), index);
        }

        ///<summary>
        ///
        ///            Return a deep copy of the persistent state, stopping at entities and at collections.
        ///            
        ///</summary>
        ///
        ///<param name="value">generally a collection element or entity field</param>
        ///<returns>
        //the value itself
        ///</returns>
        ///<remarks>
        /// since it's all singleton, the copy does not actually copy anything.
        /// </remarks>
        public object DeepCopy(object value){
            return value;
        }

        ///<summary>
        ///
        ///            During merge, replace the existing (<paramref name="target" />) value in the entity
        ///            we are merging to with a new (<paramref name="original" />) value from the detached
        ///            entity we are merging. For immutable objects, or null values, it is safe to simply
        ///            return the first parameter. For mutable objects, it is safe to return a copy of the
        ///            first parameter. For objects with component values, it might make sense to
        ///            recursively replace component values.
        ///            
        ///</summary>
        ///
        ///<param name="original">the value from the detached entity being merged</param>
        ///<param name="target">the value in the managed entity</param>
        ///<param name="owner">the managed entity</param>
        ///<returns>
        ///the value to be merged
        ///</returns>
        ///
        public object Replace(object original, object target, object owner){
            return original;
        }

        ///<summary>
        ///
        ///            Reconstruct an object from the cacheable representation. At the very least this
        ///            method should perform a deep copy if the type is mutable. (optional operation)
        ///            
        ///</summary>
        ///
        ///<param name="cached">the object to be cached</param>
        ///<param name="owner">the owner of the cached object</param>
        ///<returns>
        ///a reconstructed object from the cachable representation
        ///</returns>
        ///
        public object Assemble(object cached, object owner){
            return cached;
        }

        ///<summary>
        ///
        ///            Transform the object into its cacheable representation. At the very least this
        ///            method should perform a deep copy if the type is mutable. That may not be enough
        ///            for some implementations, however; for example, associations must be cached as
        ///            identifier values. (optional operation)
        ///            
        ///</summary>
        ///
        ///<param name="value">the object to be cached</param>
        ///<returns>
        ///a cacheable representation of the object
        ///</returns>
        ///
        public object Disassemble(object value){
            return value;
        }

        ///<summary>
        ///
        ///            The SQL types for the columns mapped by this type. 
        ///            
        ///</summary>
        ///
        public SqlType[] SqlTypes{
            get { return sqlTypes; }
        }

        ///<summary>
        ///
        ///            The type returned by 
        ///<c>NullSafeGet()</c>
        ///</summary>
        ///
        public Type ReturnedType{
            get { return typeof (object); }
        }

        ///<summary>
        ///
        ///            Are objects of this type mutable?
        ///            
        ///</summary>
        ///
        public bool IsMutable{
            get { return true; }
        }

        #endregion

        private static string GetTypeName(object value){
            Type t = value.GetType();
            string assemblyName = t.Assembly.FullName;
        int comma = assemblyName.IndexOf(",");

        assemblyName = assemblyName.Substring(0, comma);

        return t.FullName + "," + assemblyName;
        }
    }
}