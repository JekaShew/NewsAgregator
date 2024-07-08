export const loadData = () => ({
    type: 'ACCOUNTSTATUS_LOADALL',
    remote: {
        url: '/api/accountstatus/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'ACCOUNTSTATUS_REMOVE',
    remote: {
        url: '/api/accountstatus/delete/' + id,
        type: 'delete',
        apiSuccess: ['ACCOUNTSTATUS_REMOVE_SUCCESS', loadData()],
    }
});
