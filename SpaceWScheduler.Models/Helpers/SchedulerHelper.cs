using SpaceWScheduler.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWScheduler.Models.Helpers
{
    public static class SchedulerHelper
    {
        /// <summary>
        /// Fill empty fields of <paramref name="target"/> with the those of <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The object whose empty fields will be filled</param>
        /// <param name="source">the object from whom values will come to fill empty fields in <paramref name="target"/></param>
        public static void FillEmptyFields<T>(this T target, T source)
            where T : class
        {

            if (source == default) 
            {
                return;
            }

            PropertyInfo[] props = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props) 
            {
                var val = prop.GetValue(target);

                if (val == default) { 
                    prop.SetValue(target, prop.GetValue(source));
                }
            }
        }

        /// <summary>
        /// Replace fields in <paramref name="target"/> with those populated in <paramref name="source"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The object whose fields will be replaced with thsoe populated in <paramref name="source"/></param>
        /// <param name="source">The object whose populated fields will replace those in <paramref name="target"/></param>
        public static void ReplacePopulatedFields<T>(this T target, T source)
            where T : class
        {
            if (target == default || source == default) {
                return;
            }

            PropertyInfo[] props = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                var val = prop.GetValue(source);

                if (val != default)
                {
                    prop.SetValue(target, val);
                }
            }
        } 
    }
}
