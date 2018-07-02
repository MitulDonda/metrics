using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Addartifact
/// </summary>
public class Addartifact
{
    public string artNum { get; set; }
    public string artName { get; set; }
    public string desc { get; set; }
    public int toolid { get; set; }
    public int esthour { get; set; }
    public int createdby { get; set; }
    public int lockhour { get; set; }
    public string comment { get; set; }
    public string status { get; set; }
    public int AID { get; set; }
    public DateTime enddate { set; get; }
    public Addartifact()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}