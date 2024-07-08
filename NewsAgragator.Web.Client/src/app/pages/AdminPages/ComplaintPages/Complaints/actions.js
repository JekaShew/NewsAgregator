export const loadData = () => ({
    type: 'COMPLAINT_LOADALL',
    remote: {
        url: '/api/complaint/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'COMPLAINT_REMOVE',
    remote: {
        url: '/api/complaint/delete/' + id,
        type: 'delete',
        apiSuccess: ['COMPLAINT_REMOVE_SUCCESS', loadData()],
    }
});
