$(document).ready(function() {

  // create drivers' hub connection
  var connection = $.hubConnection();

  // set signalr server url
  connection.url = "http://localhost:5003/signalr"

  // choose the server hub
  var notificationsHubProxy = connection.createHubProxy('notificationsHub');

  // on new trip
  notificationsHubProxy.on("newTrip_" + , function(trip) {
    console.log(trip.Destination);

    $("ul#last-call").append(
      '<li>[' + trip.Username + '] '+ trip.Origin + " => " + trip.Destination + '</li>');
  });

  // connect to the server 
  connection.start().done(function () {
    console.log("Connected, transport = " + connection.transport.name);

    notificationsHubProxy.invoke("JoinGroup", "drivers");
  });

});