using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Views
{
    /// <summary>
    /// �Զ���ViewLocationExpander
    /// 
    /// </summary>
    public class ThemesViewLocationExpander : IViewLocationExpander
    {
        public ThemesViewLocationExpander(string theme)
        {
            this.Theme = theme;
        }

        public string Theme { get; }

        /// <summary>
        /// �⽫�������������������ͼλ��
        /// </summary>
        /// <param name="context">ͨ��context���������Է������е����������HttpContext��RouteData���ȣ�</param>
        /// <param name="viewLocations"></param>
        /// <returns></returns>
        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            var theme = context.Values["theme"];

            return viewLocations
                .Select(x => x.Replace("/Views/", "/Views/" + theme + "/"))
                .Concat(viewLocations);
        }

        /// <summary>
        /// ���ڳ�ʼ����ͼλ����չ��; ����������У����������������д�����һЩֵ
        /// </summary>
        /// <param name="context"></param>
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["theme"] = this.Theme;
        }
    }


}