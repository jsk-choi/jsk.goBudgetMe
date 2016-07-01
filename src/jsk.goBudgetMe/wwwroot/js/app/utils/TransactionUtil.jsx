import axios from 'axios';

const Transactions = {
    get (startDate, endDate) {
        return axios.get(`/api/values?startDate=${startDate}&endDate=${endDate}`);
    },
    set (tran) {
        return axios.post('/api/values', tran);
    },
    del (uid) {
        return axios.delete(`/api/values/${uid}`);
    }
};

export default Transactions;