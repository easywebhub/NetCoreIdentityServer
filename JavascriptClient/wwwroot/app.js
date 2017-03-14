function log() {
    //document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("apiwrite").addEventListener("click", apiwrite, false);
document.getElementById("logout").addEventListener("click", logout, false);

var config = {
    authority: "http://id.easyadmincp.com",
    client_id: "js",
    redirect_uri: "http://js.easyadmincp.com/callback.html",
    response_type: "id_token token",
    scope: "openid profile apis_fullaccess__read",
    post_logout_redirect_uri: "http://js.easyadmincp.com/index.html",
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in IdServer", user.profile);
        userSignInByIdSrv(user.profile.sub);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function userSignInByIdSrv(idSrvAccId) {
    log("SignIn couchbase system using IdServerAccountId");
    console.log('IdSrvAccountID', idSrvAccId);
    var url = "http://localhost:36921/auth/signin";

    var xhr = new XMLHttpRequest();
    xhr.open("POST", url);
    xhr.onload = function () {
        log(xhr.status, JSON.parse(xhr.responseText));
    }
    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    //xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
    
    xhr.send(JSON.stringify({ IdSrvAccountId: idSrvAccId }));
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "http://api.easyadmincp.com/users";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function apiwrite() {
    mgr.getUser().then(function (user) {
        var url = "http://api.easyadmincp.com/userswrite";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function logout() {
    mgr.signoutRedirect();
}