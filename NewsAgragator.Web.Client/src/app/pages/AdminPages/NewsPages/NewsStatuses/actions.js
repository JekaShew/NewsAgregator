export const loadData = () => ({
    type: 'NEWSSTATUS_LOADALL',
    remote: {
        url: '/api/newsstatus/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'NEWSSTATUS_REMOVE',
    remote: {
        url: '/api/newsstatus/delete/' + id,
        type: 'delete',
        apiSuccess: ['NEWSSTATUS_REMOVE_SUCCESS', loadData()],
    }
});
