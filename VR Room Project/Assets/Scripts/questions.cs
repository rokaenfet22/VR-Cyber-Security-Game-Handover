using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class questions
{
    public static Dictionary<string,string> cyber_threat_questions_dict = new Dictionary<string,string>
    {
        {"Stephen Nguyen was laid off last month from his executive-level position at an industrial chemical company. He worked in the research and development (R&D) department. He downloaded his latest project's information onto a personal USB flash drive. He is bitter about losing his job and considering selling the USB drive to another company's R&D department. Which type of cyber attacker group could he represent?","Malicious Insider"},
        {"Monica da Silva is an employee at an aeronautics company. She noticed her laptop has started to become unresponsive ever since she went on a business trip to a foreign country. She remembers being asked to hand the device over while at an airport and she thinks that is when the problems started. Which type of cyber attacker group could this represent?","Nation State Hacker"},
        {"She-Ra Cat is a pseudonym for a hacker who was a member of a collective European group in 2012. The group expressed solidarity with a foreign country during economic unrest, stating that the government “refused to listen to its people.” The group lodged cyber attacks against the government's websites to spread the word about the government’s failure to comply with the people’s wishes. Which type of cyber attacker group could this represent?","Hacktivist"},
        {"This attack involves software designed to perform in a detrimental manner to a target, without the target's consent. It can block access to data and programs, steal information, and make systems inoperable. What type of cyber attack is this?","Malware Attack"},
        {"This attack involves causing a system to partially crash and be unable to perform work at normal levels. What type of cyber attack is this?","DoS"},
        {"This attack involves sending an email to an individual that appears to be from a trusted source, but instead has the intention of getting personal information, such as a password. What type of cyber attack is this?","Phising"},
        {"An attack has occurred where the addresses of certain servers have been changed to the addresses of machines controlled by hackers. What type of attack is this?","DNS Attack"},
        {"Some malware has been found on an employee's laptop after they downloaded some legitimate looking software. What is this malware called?","Trojan Horse"},
        {"Some malware has been found on an employees laptop that has been installed since 2020, and has been giving a hacker access to the machine without them knowing. What is this type of malware?","Rookit"},
        {"Some malware has been found, which has been gathering information about the company, such as websites visited and usernames and passwords. What is this type of malware?","Spyware"},
        {"Some malware has been found that has been slowing down the network, and has been spreading through the network without any human assistance. What is this type of malware?","Worm"}
    };
    public static Dictionary<string,string> cyber_product_questions_dict = new Dictionary<string,string>
    {
        {"Which of the following IBM security products calculates a risk score?","Data Risk Manager"},
        {"Which of the following IBM security products starts an investigation into a potential threat for you?","QRadar SIEM"},
        {"Which of the following IBM security products allows you to add “artefacts” to threat cases, allowing you to link different cases?","SOAR"},
        {"Which software can be used to link different attacks together through artefacts such as IP addresses?","SOAR"},
        {"A piece of malware has been found! Which product will give you potential next steps to deal with the threat?","SOAR"},
        {"Which product helps you visualise potential risks to data and processes?","Data Risk Manager"},
        {"Which product will identify and prioritise potential incidents?","QRadar"},
        {"Which product gives you an insight into data on site and in the cloud?","QRadar"},
        {"Which product will start the containment process for a threat for you?","QRadar"},
        {"Which product will discover hidden connections in data?","i2 Analyst's Notebook"},
        {"Which product finds relationships between data entities to discover patterns and provides insight into data?","i2 Analyst's Notebook"}
    };
    public static Dictionary<string,string> other_questions_dict = new Dictionary<string,string>
    {
        {"How can you prevent a DNS attack?","Configure Access Control"},
        {"You have been alerted to some suspicious activity. Which product will start the investigation for you?","QRadar SIEM"},
        {"Which product can be used to check the log data?","QRadar SIEM"}
    };



    public static string[] cyber_threat_names = new string[] {
        "Malicious Insider","Hacktivist","Criminal Gang","Nation State Hacker","Spear Phishing Attack",
        "Malware Attack","DNS","DoS","Credential Stuffing Attack","MITM","SQL Injection","Phishing",
        "Rootkit","Spyware","Trojan Horse","Worm"
        };
    public static string[] cyber_product_names = new string[] {
        "QRadar","i2 Analyst's Notebook","QRadar SIEM","Data Risk Manager","SOAR","X-force Exchange"
        };
    public static string[] other_solution_names = new string[] {
        "Configure Access Control",
        "Firewall"
        };
}
