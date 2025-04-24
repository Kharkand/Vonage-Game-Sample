// Handling all of our errors here by logging them
function handleError(error) {
  if (error) {
    console.error(error.message);
  }
}

// Exposed function. Reachable from the Unity side.
function initializeSession() {

  const session = OT.initSession(applicationId, sessionId);
  
  var subscriberDiv = document.getElementById('subscriber');
  var publisherDiv = document.getElementById('publisher');

  // Subscribe to a newly created stream
  session.on('streamCreated', function(event) {
    session.subscribe(event.stream, 'subscriber', {
      insertMode: 'append',
      width: '100%',
      height: '100%'
    }, handleError);
    // Adding slide in.
    subscriberDiv.classList.add('subscriber-slide-in');
  });

  session.on("")

  // Create a publisher
  const publisher = OT.initPublisher('publisher', {
    insertMode: 'append',
    width: '100%',
    height: '100%'
  }, handleError);


  // Connect to the session
  session.connect(token, function(error) {
    if (error) {
      handleError(error);
    } else {
      session.publish(publisher, handleError);
      // Adding slide in.
      publisherDiv.classList.add('publisher-slide-in');
    }
  });
}
