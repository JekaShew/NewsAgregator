export const loadData = () => ({
    type: 'COMMENT_LOADALL',
    remote: {
        url: '/api/comment/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'COMMENT_REMOVE',
    remote: {
        url: '/api/comment/delete/' + id,
        type: 'delete',
        apiSuccess: ['COMMENT_REMOVE_SUCCESS', loadData()],
    }
});
