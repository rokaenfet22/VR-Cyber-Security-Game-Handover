using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains static dictionary of product : product information which is displayed in the "tutorial" section in Main Menu.

public class product_info : MonoBehaviour
{
    public static Dictionary<string,string> dict = new Dictionary<string,string>
    {
        {"qradar","Detect known and unknown threats\nIdentify and prioritize potential incidents\nGain visibility into enterprise data across on-premise and cloud-environments\nGain feedback to continuously improve detection, use time savings from automated security intelligence to proactively hunt threats, and automate containment processes."},
        {"i2","Track critical missions across law enforcement, fraud and financial crime, military defense, and national security and intelligence sectors with the i2 intelligence analysis platform.Make decisions in near real time by using multi dimensional visual analysis to turn data into actionable crime intelligence."},
        {"qradar_siem","Analyses log and flow data across multiple environments. Detects suspicious events in real time, correlates against vulnerabilities and generates prioritised alerts with impact and severity. Connects chain of events for you & starts investigation to discover root cause and scope of the attack. Insights into user behaviour, end point activity and network traffic"},
        {"soar","Create and manage cases inside a unified platform (where a case is a threat/ malware). Cases are automatically found so the right people are notified at the right time. Can see who owns tasks and whether tasks are open/ closed/ overdue. gives potential actions for a case. Cases have artefacts(such as IP address) that can link to other cases so you can see if the artefact links to other persistent threats"},
        {"data_risk_manager","“Bring visibility to sensitive data assets”. Focuses on critical assets. Helps unify a protection strategy around critical assets. Data visualisation. Creates a risk assessment and a risk score (%) using automated analytics"},
    };
}
