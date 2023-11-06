using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial struct BasketSystem : ISystem
{

    public void OnUpdate(ref SystemState state)
    {
        Vector3 basketPos = new Vector3(0, 0, 0);
        // getting the basket entities and sets the position to the mouse position
        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketProperties>>())
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePosition);
            basketPos = transform.ValueRW.Position;
            basketPos.x = mousePos3D.x;
            transform.ValueRW.Position = basketPos;
        }

        // getting the query for all of the apple entities
        var appleQuery = state.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<AppleGravityComponent>());

        // getting the array of apple entities and iterating through them
        using (var theApples = appleQuery.ToEntityArray(Allocator.TempJob))
        {
            foreach (var apple in theApples)
            {
                // getting position of the apple
                var applePosition = state.EntityManager.GetComponentData<LocalTransform>(apple).Position;

                // check if it hits the basket
                if (applePosition.y < basketPos.y && applePosition.x < (basketPos.x + 2) && applePosition.x > (basketPos.x - 2))
                {
                    // destroys the apple
                    state.EntityManager.DestroyEntity(apple);
                }

                // checks if the apple is no longer on the screen
                else if (applePosition.y < -20)
                {
                    // destroy apple
                    state.EntityManager.DestroyEntity(apple);

                    // getting the guery for basket, getting the array of said entities, and then destroying the first
                    var basketQuery = state.EntityManager.CreateEntityQuery(ComponentType.ReadOnly<BasketProperties>());
                    using (var baskets = basketQuery.ToEntityArray(Allocator.TempJob))
                    {
                        // getting the length of array
                        int basketLength = baskets.Length;
                        if (basketLength > 0)
                        {
                            state.EntityManager.DestroyEntity(baskets[0]);
                        }
                        else
                        {
                            SceneManager.LoadScene("StartScene");
                        }
                    }
                }
            }
        }
    }
}
