export const loadData = () => ({
    type: 'COMPLAINTTYPE_LOADALL',
    remote: {
        url: '/api/complainttype/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'COMPLAINTTYPE_REMOVE',
    remote: {
        url: '/api/complainttype/delete/' + id,
        type: 'delete',
        apiSuccess: ['COMPLAINTTYPE_REMOVE_SUCCESS', loadData()],
    }
});
