using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static bool initialized;
    public static int randomOpenSlotIndex;

    public Slotscript slot1;
    public Slotscript slot2;
    public Slotscript slot3;
    public Slotscript slot4;
    public Slotscript slot5;
    public Slotscript slot6;
    public Slotscript slot7;
    public Slotscript slot8;

    public static List<Slotscript> Slots;
    public static List<Slotscript> OpenSlots;

    // Start is called before the first frame update
    void Start()
    {
        Slots = new List<Slotscript>();
        OpenSlots = new List<Slotscript>();

        Slots.Add(slot1);
        Slots.Add(slot2);
        Slots.Add(slot3);
        Slots.Add(slot4);
        Slots.Add(slot5);
        Slots.Add(slot6);
        Slots.Add(slot7);
        Slots.Add(slot8);

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(Slots.Count);

        foreach (Slotscript slot in Slots)
        {
//            Debug.Log(slot.name);

            if (slot != null)
            {
                if (!slot.isOccupied && (!OpenSlots.Contains(slot) || OpenSlots == null))
                {
//                    Debug.Log(slot + "added");

                    OpenSlots.Add(slot);
                }
                else if (slot.isOccupied && OpenSlots.Contains(slot))
                {
//                    Debug.Log(slot + "removed");

                    OpenSlots.Remove(slot);
                }
            }
        }

        if (initialized)
        {
            randomOpenSlotIndex = Random.Range(0, OpenSlots.Count);
        }
    }
}
