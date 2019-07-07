using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace InternetToolsV6.Networking.Traceroute
{
    public class Traceroute : ITraceroute
    {
        public IEnumerable<TracerouteDataStructure> SendTraceRoute(string Address, int Timeout, int MaxHops)
        {
            TracerouteDataStructure tds = new TracerouteDataStructure();
            //yield return tds;
            string hostname = Address;
            // following are the defaults for the "traceroute" command in unix.
            //timeout before going to another thing
            int timeout = Timeout;
            //max number of servers/hops to jump through before stopping
            int maxTTL = MaxHops;
            int currentmaxttl = 0;
            Stopwatch g = new Stopwatch();
            Stopwatch g2 = new Stopwatch();
            byte[] buffer =
            {
                101, 108, 108, 111, 32, 116, 104, 101,
                114, 101, 32, 99, 97, 110, 32, 121,
                111, 117, 32, 115, 101, 101, 32, 116,
                104, 105, 115, 32, 112, 105, 110, 103
            };
            System.Net.NetworkInformation.Ping pinger = new System.Net.NetworkInformation.Ping();
            //PingStatusText = $"Traceroute Started on {hostname}" + Environment.NewLine;
            for (int ttl = 1; ttl <= maxTTL; ttl++)
            {
                currentmaxttl++;
                g.Start();
                g2.Start();
                PingOptions options = new PingOptions(ttl, true);
                PingReply reply = null;
                bool err = false;
                //send ping
                try
                {
                    reply = pinger.Send(hostname, timeout, buffer, options);
                }
                catch (PingException es)
                {
                    tds.Status = "PingException. Stopping..."; err = true;
                    tds.Route = $"{ es.Message}";  break;
                }
                catch (ArgumentException es)
                {
                    tds.Status = "Argument Exception. Stopping..."; err = true;
                    tds.Route = $"{ es.Message} { es.ParamName}";  break;
                }
                catch (Exception exc)
                {
                    tds.Status = "Generat Error Stopping..."; err = true;
                    tds.Route = $"{ exc.Message}"; break;
                }
                if (err)
                    yield return tds;
                if (reply != null)
                {
                    tds.AddressPosition = $"{ttl}";
                    tds.Interval = $"{g2.ElapsedMilliseconds} ms";
                    tds.Route = $"{reply.Address}";
                    tds.TotalTime = $"{g.ElapsedMilliseconds}";
                    if (reply.Status == IPStatus.TtlExpired)
                    {
                        // TtlExpired means we've found an address, but there are more addresses
                        tds.Status = "Success ->";
                        g2.Reset(); yield return tds;
                        continue;
                    }
                    if (reply.Status == IPStatus.BadRoute)
                    {
                        tds.Status = "Bad Route ->";
                    }
                    if (reply.Status == IPStatus.BadDestination)
                    {
                        tds.Status = "Bad Destination ->";
                    }
                    if (reply.Status == IPStatus.DestinationHostUnreachable)
                    {
                        tds.Status = "Bad Destination ->";
                    }
                    if (reply.Status == IPStatus.DestinationPortUnreachable)
                    {
                        tds.Status = "Bad Destination ->";
                    }
                    if (reply.Status == IPStatus.ParameterProblem)
                    {
                        tds.Status = "Bad Destination ->";
                    }
                    if (reply.Status == IPStatus.TimedOut)
                    {
                        // TimedOut means this ttl is no good, we should continue searching
                        tds.Status = "Timeout"; yield return tds;
                        continue;
                    }
                    if (reply.Status == IPStatus.Success)
                    {
                        // Success means the tracert has completed
                        //traceRouteViewModel.AddNewMessage($" -- ICMP Trace route completed : {reply.Address} in {g.ElapsedMilliseconds} ms --");
                        tds.Status = "ICMP Traceroute Complete";
                        //PingStatusText = "Traceroute Completed" + Environment.NewLine;
                        //PingStats += $"TraceRoute Completed." + Environment.NewLine;
                        g.Stop();
                    }
                    yield return tds;

                }
                // if we ever reach here, we're finished, so break
                break;
            }
            if (currentmaxttl >= maxTTL)
            {
                tds.Status = "Max Hops Exceeded. Stopping...";
                yield return tds;
                //PingStatusText = $"Traceroute Stopped. Exceded {maxTTL} Hops." + Environment.NewLine;
            }
        }
    }
}