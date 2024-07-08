export const loadData = () => ({
    type: 'POLICY_LOADALL',
    remote: {
        url: '/api/policy/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'POLICY_REMOVE',
    remote: {
        url: '/api/policy/delete/' + id,
        type: 'delete',
        apiSuccess: ['POLICY_REMOVE_SUCCESS', loadData()],
    }
});
