using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InsuranceDefine
{
    public string insuranceName { get; set; }
    public int isPartnered { get; set; }
    public int needReferral { get; set; }
    public int hasPlans { get; set; }
    public string otherRequire { get; set; }

    public override string ToString()
    {
        return insuranceName;
    }


}

