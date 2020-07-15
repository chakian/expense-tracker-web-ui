import { authHeader } from '../_helpers/auth-header';
import { IUserState } from '../user/reducer';

var Config = require('Config');

export const userService = {
    login,
    logout
};

function login(email, password): Promise<IUserState> {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
    };
    
    return fetch(`${Config.apiUrl}/user/login`, requestOptions)
        .then(handleResponse)
        .then(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('user', JSON.stringify(user.user));

            const myUser: IUserState = {
                email: user.user.email,
                name: user.user.name,
                token: user.user.token,
                defaultBudgetId: user.user.defaultBudgetId
            };
            return myUser;
        });
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                logout();
                // location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}