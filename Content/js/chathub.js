const connection = new signalR.HubConnectionBuilder()
    .withUrl("/ChatHub")
    .build();
try {
    connection.start().then(() => {
        const token = document.getElementById("token").value;
        connection.invoke("JoinChatHub", token).catch((err) => {
            console.log("Failed to join the chat hub:")
            console.log(err);
        });
    })
} catch (err) {
    console.log("Failed to connect chat hub");
    connection.log(err);
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        console.error(err.toString());
    });
    event.preventDefault();
});

connection.on("ReceiveMessage", function (user, message) {
    const messageList = document.getElementById("messagesList");
    const li = document.createElement("li");
    li.textContent = `${user}: ${message}`;
    messageList.appendChild(li);
});
