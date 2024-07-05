export const loadData = () => ({
    type: 'POLICY_LOADALL',
    remote: {
        url: '/api/policy/takeall',
        type: 'get',
    }
});
