let cachedData = null;
let cachedPostData = null;

const postServiceData = (url, params) => {
  console.log('cache status' + cachedPostData );
  if (cachedPostData === null) {
    console.log('post-data: requesting data');
    return fetch(url, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(params)
    })
      .then(response => {
        cachedPostData = response.json();
        return cachedPostData;
      });
  } else {
    console.log('post-data: returning cachedPostData data');
    return Promise.resolve(cachedPostData);
  }
}

const getServiceData = (url) => {
  console.log('cache status' + cachedData );
  if (cachedData === null) {
    console.log('get-data: requesting data');
    return fetch(url, {})
      .then(response => {
        cachedData = response.json();
        return cachedData;
      });
  } else {
    console.log('get-data: returning cached data');
    return Promise.resolve(cachedData);
  }
};

export default {
  getServiceData, 
  postServiceData 
};
