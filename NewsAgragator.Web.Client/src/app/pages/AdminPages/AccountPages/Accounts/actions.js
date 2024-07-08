export const loadData = () => ({
    type: 'ACCOUNT_LOADALL',
    remote: {
        url: '/api/account/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'ACCOUNT_REMOVE',
    remote: {
        url: '/api/account/delete/' + id,
        type: 'delete',
        apiSuccess: ['ACCOUNT_REMOVE_SUCCESS', loadData()],
    }
});
