$('#calendar').fullCalendar({
    header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay,listWeek'
    },
   
    navLinks: true,
    editable: true,
    eventLimit: true,
    events:'/Home/GetEvents'
});