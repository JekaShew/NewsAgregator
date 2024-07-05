export const loadData = () => ({
    type: 'ACCOUNTSTATUS_LOADALL',
    remote: {
        url: '/api/accountstatus/takeall',
        type: 'get',
    }
});
