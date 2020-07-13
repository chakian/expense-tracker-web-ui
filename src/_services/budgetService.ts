import { IBudgetListState } from "../budget/reducer";

export function getBudgetList(token: string): Promise<IBudgetListState> {
    const requestOptions = {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
        }
    };
    //http://localhost:8000/api/v1
    // return fetch(`${config.apiUrl}/user/login`, requestOptions)
    return fetch(`http://localhost:8000/api/v1/budget`, requestOptions)
        .then(handleResponse)
        .then(bl => {
            const list: IBudgetListState = { 
                list: bl.data
            };
            return list;
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
