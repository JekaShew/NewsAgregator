export const loadData = () => ({
    type: 'COMPLAINTSTATUS_LOADALL',
    remote: {
        url: '/api/complaintstatus/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'COMPLAINTSTATUS_REMOVE',
    remote: {
        url: '/api/complaintstatus/delete/' + id,
        type: 'delete',
        apiSuccess: ['COMPLAINTSTATUS_REMOVE_SUCCESS', loadData()],
    }
});
