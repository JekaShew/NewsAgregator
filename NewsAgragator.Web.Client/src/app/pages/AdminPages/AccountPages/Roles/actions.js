export const loadData = () => ({
    type: 'ROLE_LOADALL',
    remote: {
        url: '/api/role/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'ROLE_REMOVE',
    remote: {
        url: '/api/role/delete/' + id,
        type: 'delete',
        apiSuccess: ['ROLE_REMOVE_SUCCESS', loadData()],
    }
});
