

document.getElementById("signin").addEventListener("click",function(e) {
        const username = document.getElementById("username").value;
        SignIn(username, "");
        e.preventDefault();
    }
);
function SignIn(username,password) {

    const data = {
        username: username,
        password: password
    };

    fetch( '/api/TokenApi/SignIn', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
        .then((response) => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Authentication failed');
            }
        })
        .then((data) => {
            const token = data.token;
            const id = data.id;
            console.log('Received token:', token,"Id: ",id);
            document.getElementById("token").value = token;
            document.getElementById("chat-form").submit();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}
