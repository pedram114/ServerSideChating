
        function websocketconnect(){
            debugger;
            var socket;
            var host = "ws://localhost:9990";

            try{
                var socket = new WebSocket(host);

                message('<p class="event">Socket Status: '+socket.readyState);

                socket.onopen = function(){
                    message('<p class="event">Socket Status: '+socket.readyState+' (open)');
                }

                socket.onmessage = function(msg){
                    message('<p class="message">Received: '+msg.data);
                }

                socket.onclose = function(){
                    message('<p class="event">Socket Status: '+socket.readyState+' (Closed)');
                }

            } catch(exception){
                message('<p>Error'+exception);
            }

            function send(){
                var text = $('#text').val();

                if(text==""){
                    message('<p class="warning">Please enter a message');
                    return ;
                }
                try{
                    socket.send(text);
                    message('<p class="event">Sent: '+text)

                } catch(exception){
                    message('<p class="warning">');
                }
                $('#text').val("");
            }

            function message(msg){
                $('#chatLog').append(msg+'</p>');
            }

            $('#text').keypress(function(event) {
                if (event.keyCode == '13') {
                    send();
                }
            });

            $('#disconnect').click(function(){
                socket.close();
            });

        }//End connect

  