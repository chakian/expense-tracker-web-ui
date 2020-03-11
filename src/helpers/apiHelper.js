// let cachedData = null;
// let cachedPostData = null;
let baseUrl = 'http://localhost:8000/api/v1';

const postServiceData = (url, params) => {
  //TODO: This cache mechanism seems nice. I will find a way to use it properly
  //console.log('cache status' + cachedPostData );
  //if (cachedPostData === null) {
  console.log('post-data: requesting data');
  return fetch(baseUrl + url, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(params)
  })
    .then(response => {
      // cachedPostData = response.json();
      return response.json();
      // return cachedPostData;
    });
  // } else {
  //   console.log('post-data: returning cachedPostData data');
  //   return Promise.resolve(cachedPostData);
  // }
}

const getServiceData = (url) => {
  //TODO: This cache mechanism seems nice. I will find a way to use it properly
  // console.log('cache status' + cachedData);
  // if (cachedData === null) {
  console.log('get-data: requesting data');
  return fetch(baseUrl + url, {})
    .then(response => {
      // cachedData = response.json();
      return response.json();
      // return cachedData;
    });
  // } else {
  //   console.log('get-data: returning cached data');
  //   return Promise.resolve(cachedData);
  // }
};

export default {
  getServiceData,
  postServiceData
};
