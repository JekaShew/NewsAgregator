import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "POLICY_LOADALL_SUCCESS":
            return {
                ...state,
                policies:
                {
                    loading: false,
                    value: action.data,
                }
            }

        case "POLICY_LOADALL_START":
            return {
                ...state,
                policies:
                {
                    loading: true,
                    value:
                        [
                            {
                                id: {
                                    value: '',
                                },
                                title: {
                                    value: '',
                                },
                                description: {
                                    value: '',
                                },
                            },
                        ],
                }
            }

        case "POLICY_REMOVE_START":
            return {
                ...state,
                policies:
                {
                    ...state.policies,
                    loading: true,
                }
            }

        default:
            return state
    }
}