import axios from 'axios';

const fetchImages = (pageNumber) => {
    let res = [];

    axios
        .get(
            `${process.env.VUE_APP_BACKEND_API}/images?pageNumber=${pageNumber}&pageSize=1`
        )
        .then((response) => {
            res = response.data.payload
        });

    return res;
}

export default fetchImages;