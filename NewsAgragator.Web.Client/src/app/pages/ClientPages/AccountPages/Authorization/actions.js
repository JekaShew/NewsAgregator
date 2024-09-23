export const signIn = (signINModel) => ({
    type: 'AUTHORIZATION_SIGN_IN',
    remote: {
        url: "/api/authorization/signin",
        type: 'post',
        body: signINModel
    }
});

export const signUP = (signUPModel) => ({
    type: 'AUTHORIZATION_SIGN_UP',
    remote: {
        url: "/api/authorization/signup",
        type: 'post',
        body: signUPModel
    }
});

export const revoke = () => ({
    type: 'AUTHORIZATION_REVOKE',
    remote: {
        url: "/api/authorization/revoke" + JSON.parse(localStorage.getItem("AUTHORIZATION")).rtoken,
        type: 'post',
        body: signINModel
    }
});

export const refresh = () => ({
    type: 'AUTHORIZATION_REFRESH',
    remote: {
        url: "/api/authorization/refresh",
        type: 'post',
        body: JSON.parse(localStorage.getItem("AUTHORIZATION")).rtoken
    }
});

export const signOut = () => ({
    type: 'AUTHORIZATION_SIGN_OUT',
    remote: {
        url: "/api/authorization/signout",
        type: 'post',
        body: JSON.parse(localStorage.getItem("AUTHORIZATION")).rtoken
    }
});