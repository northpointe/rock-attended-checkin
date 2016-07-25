using System;
using System.Collections.Generic;
using System.Linq;
using Rock.Data;
using Rock.Model;
using Rock.CheckIn;
using System.Text;
using System.Threading.Tasks;

namespace cc.newspring.AttendedCheckIn.Utility
{
    public class ScheduleAttendance
    {
        /// <summary>
        /// A list of attendance counts per schedule
        /// </summary>
        private List<SchedulePair> ScheduleAttendanceList = new List<SchedulePair>();

        /// <summary>
        /// Gets the attendance count for all of the schedules for a location. This will show on the schedule buttons.
        /// </summary>
        /// <param name="location"></param>
        protected void GetScheduleAttendance( CheckInLocation location )
        {
            if ( location != null )
            {
                var rockContext = new RockContext();
                var attendanceService = new AttendanceService( rockContext );
                var attendanceQuery = attendanceService.GetByDateAndLocation( DateTime.Now, location.Location.Id );

                ScheduleAttendanceList.Clear();
                foreach ( var schedule in location.Schedules )
                {
                    var attendance = new SchedulePair();
                    attendance.ScheduleId = schedule.Schedule.Id;
                    attendance.AttendanceCount = attendanceQuery.Where( l => l.ScheduleId == attendance.ScheduleId ).Count();
                    ScheduleAttendanceList.Add( attendance );
                }
            }
        }
    }

    /// <summary>
    /// A container for a schedule and attendance count
    /// </summary>
    public class SchedulePair
    {
        
        

        public int ScheduleId { get; set; }

        public int AttendanceCount { get; set; }

        
    }
}
