export const loadData = () => ({
    type: 'ACCOUNT_LOADALL',
    remote: {
        url: '/api/account/takeall',
        type: 'get',
    }
});
